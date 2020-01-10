namespace Yugen.Toolkit.Standard.Printer.UsbPos
{
    /// <summary>
    /// A listing of ASCII control characters for readability.
    /// </summary>
    public static class AsciiControlChars
    {
        /// <summary>
        /// Usually indicates the end of a string.
        /// </summary>
        public const char Nul = (char)0x00;

        /// <summary>
        /// Meant to be used for printers. When receiving this code the 
        /// printer moves to the next sheet of paper.
        /// </summary>
        public const char FormFeed = (char)0x0C;

        /// <summary>
        /// Starts an extended sequence of control codes.
        /// </summary>
        public const char Escape = (char)0x1B;

        /// <summary>
        /// Advances to the next line.
        /// </summary>
        public const char Newline = (char)0x0A;

        /// <summary>
        /// Defined to separate tables or different sets of data in a serial
        /// data storage system.
        /// </summary>
        public const char GroupSeparator = (char)0x1D;

        /// <summary>
        /// A horizontal tab.
        /// </summary>
        public const char HorizontalTab = (char)0x09;


        /// <summary>
        /// Vertical Tab
        /// </summary>
        public const char VerticalTab = (char)0x11;


        /// <summary>
        /// Returns the carriage to the start of the line.
        /// </summary>
        public const char CarriageReturn = (char)0x0D;

        /// <summary>
        /// Cancels the operation.
        /// </summary>
        public const char Cancel = (char)0x18;

        /// <summary>
        /// Indicates that control characters present in the stream should
        /// be passed through as transmitted and not interpreted as control
        /// characters.
        /// </summary>
        public const char DataLinkEscape = (char)0x10;

        /// <summary>
        /// Signals the end of a transmission.
        /// </summary>
        public const char EndOfTransmission = (char)0x04;

        /// <summary>
        /// In serial storage, signals the separation of two files.
        /// </summary>
        public const char FileSeparator = (char)0x1C;

    }
}
