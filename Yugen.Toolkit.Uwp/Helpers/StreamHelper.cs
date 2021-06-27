using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class StreamHelper
    {
        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                string content = reader.ReadToEnd();
                return content;
            }
        }

        public static async Task<string> StreamToString(IInputStream inputStream)
        {
            DataReader sr = new DataReader(inputStream) { InputStreamOptions = InputStreamOptions.Partial };

            await sr.LoadAsync(1024);
            return sr.ReadString(sr.UnconsumedBufferLength);
        }

        public static string StreamToString2(IInputStream inputStream)
        {
            var stream = inputStream.AsStreamForRead();
            string content;
            using (StreamReader reader = new StreamReader(stream))
            {
                content = reader.ReadToEnd();
            }
            return content;
        }

        public static async Task<string> StreamToString3(IInputStream inputStream)
        {
            var requestString = string.Empty;
            uint BufferSize = 2 << 17;
            using (IInputStream input = inputStream)
            {
                byte[] data = new byte[BufferSize];
                IBuffer buffer = data.AsBuffer();
                uint dataRead = BufferSize;
                while (dataRead == BufferSize)
                {
                    await input.ReadAsync(buffer, BufferSize, InputStreamOptions.Partial);
                    requestString = Encoding.UTF8.GetString(data, 0, data.Length);
                    dataRead = buffer.Length;
                }
            }

            return requestString;
        }

        //public static async Task<InMemoryRandomAccessStream> StreamToInMemoryRandomAccessStream(IClosableStream inputStream)
        //{
        //    var readSize = (uint)inputStream.Size;
        //    var reader = new DataReader(inputStream);
        //    var actualReadBytes = await reader.LoadAsync(readSize);
        //    var contentBuffer = reader.DetachBuffer();
        //    inputStream.Close();

        //    InMemoryRandomAccessStream outputStream = new InMemoryRandomAccessStream();
        //    var writer = new DataWriter(outputStream.GetOutputStreamAt(0));
        //    writer.WriteBuffer(contentBuffer);
        //    var wroteBytes = await writer.StoreAsync();
        //    return outputStream;
        //}

        public static byte[] StreamToByteArray(Stream inputStream)
        {
            if (inputStream is MemoryStream stream)
                return stream.ToArray();

            using (var memoryStream = new MemoryStream())
            {
                inputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static byte[] StreamToByteArray(IRandomAccessStreamWithContentType inputStream)
        {
            var classicStream = inputStream.AsStreamForRead();
            return StreamHelper.StreamToByteArray(classicStream);
        }


        public static async Task<byte[]> FileToByteArray(StorageFile file)
        {
            var stream = await file.OpenReadAsync();
            return StreamToByteArray(stream);
        }

        public static async Task<InMemoryRandomAccessStream> FileToMemoryStream(StorageFile file)
        {
            var inputStream = await file.OpenStreamForReadAsync();
            inputStream.Position = 0;
            byte[] buf = new byte[inputStream.Length];
            await inputStream.ReadAsync(buf, 0, (int)inputStream.Length);

            InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream();
            await memoryStream.WriteAsync(buf.AsBuffer());
            memoryStream.Seek(0);

            return memoryStream;
        }

        //Not Tested
        //public static async Task<InMemoryRandomAccessStream> FileToMemoryStream(StorageFile sf)
        //{
        //    InMemoryRandomAccessStream ras = new InMemoryRandomAccessStream();
        //    using (Stream stream = await sf.OpenStreamForReadAsync())
        //    {
        //        await stream.CopyToAsync(ras.AsStreamForWrite());
        //    }
        //    return ras;
        //}

        public static async Task<string> FileToString(StorageFile file)
        {
            var stream = await FileToStream(file);
            return StreamToString(stream);
        }

        public static async Task<string> FileToString2(StorageFile file)
        {
            StringBuilder response = new StringBuilder();

            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                while (streamReader.Peek() >= 0)
                {
                    response.Append(streamReader.ReadLine());
                }
            }

            return response.ToString();
        }

        public static async Task<Stream> FileToStream(StorageFile file)
        {
            var inputStream = await file.OpenReadAsync();
            return inputStream.AsStreamForRead();
        }


        public static string MemoryStreamToString(InMemoryRandomAccessStream memoryStream)
        {
            var stream = memoryStream.AsStream();
            return StreamHelper.StreamToString(stream);
        }

        //Not Tested
        //public static string MemoryStreamToString(MemoryStream memoryStream)
        //{
        //    return Encoding.UTF8.GetString(memoryStream.ToArray());
        //}


        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }

        public static async Task<InMemoryRandomAccessStream> StringToMemoryStream(string text)
        {
            InMemoryRandomAccessStream outputStream = new InMemoryRandomAccessStream();
            DataWriter writer = new DataWriter(outputStream.GetOutputStreamAt(0));
            writer.WriteString(text);
            await writer.StoreAsync();
            return outputStream;
        }

        public static byte[] StringToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }


        public static string BytesToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }


        public static async Task WriteStreamToStream(IRandomAccessStreamWithContentType inputRandomAccessStream, IOutputStream outputStream)
        {
            using (var writeOutputStream = outputStream.AsStreamForWrite())
            using (var inputStream = inputRandomAccessStream.AsStreamForRead())
            {
                await WriteStreamToStream(inputStream, writeOutputStream);
            }
        }

        public static async Task WriteStreamToStream(Stream inputStream, IOutputStream outputStream)
        {
            using (var writeOutputStream = outputStream.AsStreamForWrite())
            {
                await WriteStreamToStream(inputStream, writeOutputStream);
            }
        }

        public static async Task WriteStreamToStream(IRandomAccessStream inputStream, Stream outputStream)
        {
            var input = inputStream.AsStreamForRead();
            await WriteStreamToStream(input, outputStream);
        }

        public static async Task WriteStreamToStream(Stream inputStream, Stream outputStream)
        {
            if (inputStream.Length == 0)
            {
                //LoggerHelper.WriteLine(typeof(StreamHelper), "inputStream.Length == 0");
            }
            else if (outputStream.CanWrite)
            {
                await inputStream.CopyToAsync(outputStream);
                await outputStream.FlushAsync();
            }
            else
            {
                //LoggerHelper.WriteLine(typeof(StreamHelper), "stream closed");
            }
        }

        public static async Task WriteStreamToStreamOld(Stream inputStream, Stream outputStream)
        {
            var bytes = new byte[1 << 16]; // 64k
            int count;
            while ((count = inputStream.Read(bytes, 0, bytes.Length)) > 0)
            {
                await outputStream.WriteAsync(bytes, 0, count);
            }

            await outputStream.FlushAsync();
        }


        public static async Task WriteStringToStream(string text, Stream outputStream)
        {
            var textArray = Encoding.UTF8.GetBytes(text);
            if (outputStream.CanWrite)
            {
                await outputStream.WriteAsync(textArray, 0, textArray.Length);
            }
            else
            {
                //LoggerHelper.WriteLine(typeof(StreamHelper), "stream closed");
            }
        }


        //Not Tested
        //public static byte[] StreamToByteArray(Stream stream)
        //{
        //    return stream.ReadToEnd();
        //}

        //public static byte[] ReadToEnd(Stream stream)
        //{
        //    long originalPosition = 0;
        //    if (stream.CanSeek)
        //    {
        //        originalPosition = stream.Position;
        //        stream.Position = 0;
        //    }
        //    try
        //    {
        //        byte[] readBuffer = new byte[4096];

        //        int totalBytesRead = 0;
        //        int bytesRead;

        //        while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
        //        {
        //            totalBytesRead += bytesRead;

        //            if (totalBytesRead == readBuffer.Length)
        //            {
        //                int nextByte = stream.ReadByte();
        //                if (nextByte != -1)
        //                {
        //                    byte[] temp = new byte[readBuffer.Length * 2];
        //                    System.Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
        //                    System.Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
        //                    readBuffer = temp;
        //                    totalBytesRead++;
        //                }
        //            }
        //        }

        //        byte[] buffer = readBuffer;
        //        if (readBuffer.Length != totalBytesRead)
        //        {
        //            buffer = new byte[totalBytesRead];
        //            System.Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
        //        }
        //        return buffer;
        //    }
        //    finally
        //    {
        //        if (stream.CanSeek)
        //        {
        //            stream.Position = originalPosition;
        //        }
        //    }
        //}
    }
}