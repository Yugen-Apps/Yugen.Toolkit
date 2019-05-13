namespace Common.Uwp.Controls.CustomDialog
{
    /// <summary>
    /// Dialog display type
    /// </summary>
    public enum CustomDialogTypeEnum
    {
        /// <summary>
        /// Only the top bar visible
        /// </summary>
        OnlyTopBar,
        /// <summary>
        /// Both bar visibles with both buttons
        /// </summary>
        TopBarWithButtons,
        /// <summary>
        /// Both bar visibles with only right button visible
        /// </summary>
        TopBarAcceptButton,
        /// <summary>
        /// only content visible
        /// </summary>
        Nothing
    }
}