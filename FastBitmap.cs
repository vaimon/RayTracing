using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace FastBitmap
{
    public unsafe class FastBitmap : IDisposable
    {
        public readonly int Width;

        public readonly int Height;

        public int Count => Height * Width;

        /// <summary>
        /// Bitmap, из которого был создан этот экземпляр FastBitmap.
        /// Хранится, чтобы потом можно было разблокировать.
        /// </summary>
        private readonly Bitmap _source;

        /// <summary>
        /// Данные о Bitmap.
        /// Хранится, чтобы потом можно было разблокировать.
        /// </summary>
        private readonly BitmapData _bitmapData;

        /// <summary>
        /// Количество байт, отведённое на один пиксель в формате изображения.
        /// </summary>
        private readonly int _bytesPerPixel;

        /// <summary>
        /// Количество байт, отведённое на одну строку в формате изображения.
        /// </summary>
        private readonly int _stride;

        /// <summary>
        /// Указатель на начало байтового потока, которым закодирована картинка.
        /// </summary>
        private readonly byte* _scan0;

        /// <summary>
        /// PixelFormat, к которому мы переводим любой Bitmap.
        /// </summary>
        private const PixelFormat TargetPixelFormat = PixelFormat.Format32bppArgb;

        public FastBitmap(Bitmap bitmap)
        {
            Width = bitmap.Width;
            Height = bitmap.Height;
            _source = bitmap;
            // Блокируем данные о Bitmap в оперативной памяти
            _bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                TargetPixelFormat
            );
            _stride = _bitmapData.Stride;
            _bytesPerPixel = Image.GetPixelFormatSize(TargetPixelFormat) / 8;
            _scan0 = (byte*) _bitmapData.Scan0.ToPointer();
        }

        /// <summary>
        /// Возвращает указатель на расположение пикселя в памяти
        /// </summary>
        /// <param name="point">Координаты пикселя</param>
        private byte* PixelOffset(Point point)
            =>
                _scan0 // Указатель на начало потока байтов Bitmap
                + point.Y * _stride // Сдвиг Y-той строки
                + point.X * _bytesPerPixel; // Сдвиг X-того пикселя

        public void SetPixel(Point point, Color color)
        {
            if(point.X >= Width || point.X <= 0 || point.Y >= Height || point.Y <= 0)
            {
                return;
            }
            var data = PixelOffset(point);
            data[Channel.A] = color.A;
            data[Channel.R] = color.R;
            data[Channel.G] = color.G;
            data[Channel.B] = color.B;
        }

        public Color GetPixel(Point point)
        {
            var data = PixelOffset(point);
            return Color.FromArgb(
                data[Channel.A],
                data[Channel.R],
                data[Channel.G],
                data[Channel.B]
            );
        }

        public Color this[int x, int y]
        {
            get => GetPixel(new Point(x, y));
            set => SetPixel(new Point(x, y), value);
        }

        /// <summary>
        /// Перемещает данные Bitmap обратно в видеопамять.
        /// При использовании using вызывается автоматически.
        /// </summary>
        public void Dispose()
        {
            _source.UnlockBits(_bitmapData);
        }

        private static class Channel
        {
            public const int A = 3;
            public const int R = 2;
            public const int G = 1;
            public const int B = 0;
        }
    }

    public static class FastBitmapTools
    {
        /// <summary>
        /// Совершает какое-то действие с каждым пикселем заданного Bitmap.
        /// </summary>
        /// <param name="source">Bitmap, пиксели которого обходятся.</param>
        /// <param name="action">Действие, совершаемое над пикселями.</param>
        public static void ForEach(this Bitmap source, Action<Color> action)
        {
            using (var fastSource = new FastBitmap(source))
                fastSource.ForEach(action);
        }

        /// <summary>
        /// Совершает какое-то действие с каждым пикселем заданного FastBitmap.
        /// </summary>
        /// <param name="source">Bitmap, пиксели которого обходятся.</param>
        /// <param name="action">Действие, совершаемое над пикселями.</param>
        public static void ForEach(this FastBitmap source, Action<Color> action)
        {
            for (var y = 0; y < source.Height; y += 1)
            for (var x = 0; x < source.Width; x += 1)
                action(source[x, y]);
        }
        
        /// <summary>
        /// Возвращает новую Bitmap, все пиксели которой получены преобразованием пикселей
        /// заданного Bitmap определённым образои.
        /// </summary>
        /// <param name="source">Искходны Bitmap.</param>
        /// <param name="transform">Преобразование, применяемое к пикселям исходного битмапа.</param>
        public static Bitmap Select(this Bitmap source, Func<Color, Color> transform)
        {
            using (var fastSource = new FastBitmap(source))
                return fastSource.Select(transform);
        }

        /// <summary>
        /// Возвращает новую Bitmap, все пиксели которой получены преобразованием пикселей
        /// заданного FastBitmap определённым образои.
        /// </summary>
        /// <param name="source">Искходны Bitmap.</param>
        /// <param name="transform">Преобразование, применяемое к пикселям исходного битмапа.</param>
        public static Bitmap Select(this FastBitmap source, Func<Color, Color> transform)
        {
            var result = new Bitmap(source.Width, source.Height);

            using (var fastResult = new FastBitmap(result))
            {
                for (var y = 0; y < fastResult.Height; y += 1)
                for (var x = 0; x < fastResult.Width; x += 1)
                    fastResult[x, y] = transform(source[x, y]);
            }

            return result;
        }
    }
}
