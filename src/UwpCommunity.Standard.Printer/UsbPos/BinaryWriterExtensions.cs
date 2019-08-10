using System.IO;

namespace UwpCommunity.Standard.Printer.UsbPos
{
    public static class BinaryWriterExtensions
    {
        public static void Enlarged(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)32);
            bw.Write(text);
            bw.Write(AsciiControlChars.Newline);
        }

        public static void High(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)16);
            bw.Write(text); //Width,enlarged
            bw.Write(AsciiControlChars.Newline);
        }

        public static void LargeText(this BinaryWriter bw, string text)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)48);
            bw.Write(text);
            bw.Write(AsciiControlChars.Newline);
        }

        public static void FeedLines(this BinaryWriter bw, int lines)
        {
            bw.Write(AsciiControlChars.Newline);
            if (lines <= 0) return;
            bw.Write(AsciiControlChars.Escape);
            bw.Write('d');
            bw.Write((byte)lines - 1);
        }

        public static void Finish(this BinaryWriter bw)
        {
            bw.FeedLines(1);
            bw.NormalFont("---  Thank You, Come Again ---");
            bw.FeedLines(1);
            bw.Write(AsciiControlChars.Newline);
        }

        public static void NormalFont(this BinaryWriter bw, string text, bool line = true)
        {
            bw.Write(AsciiControlChars.Escape);
            bw.Write((byte)33);
            bw.Write((byte)8);
            bw.Write(" " + text);
            if (line)
                bw.Write(AsciiControlChars.Newline);
        }
    }

    /*
    27 33 0     ESC ! NUL    Master style: pica                              ESC/P
    27 33 1     ESC ! SOH    Master style: elite                             ESC/P
    27 33 2     ESC ! STX    Master style: proportional                      ESC/P
    27 33 4     ESC ! EOT    Master style: condensed                         ESC/P
    27 33 8     ESC ! BS     Master style: emphasised                        ESC/P
    27 33 16    ESC ! DLE    Master style: enhanced (double-strike)          ESC/P
    27 33 32    ESC ! SP     Master style: enlarged (double-width)           ESC/P
    27 33 64    ESC ! @      Master style: italic                            ESC/P
    27 33 128   ESC ! ---    Master style: underline                         ESC/P
                 Above values can be added for combined styles.

    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)0);
    bw.Write("test"); //Default, Pica
    bw.Write(AsciiControlChars.Newline);

    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)4);
    bw.Write("test"); //condensed
    bw.Write(AsciiControlChars.Newline);
    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)8);
    bw.Write("test"); //emphasised
    bw.Write(AsciiControlChars.Newline);
    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)16);
    bw.Write("test"); //Height,enhanced
    bw.Write(AsciiControlChars.Newline);
    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)32);
    bw.Write("test"); //Width,enlarged
    bw.Write(AsciiControlChars.Newline);
    bw.Write(AsciiControlChars.Escape);
    bw.Write((byte)33);
    bw.Write((byte)48);
    bw.Write("test");   //WxH
    bw.Write(AsciiControlChars.Newline);
    */
}