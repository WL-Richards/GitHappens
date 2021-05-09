using GitHappens.Properties;
using stdole;
namespace GitHappens
{

    /// <summary>
    /// General static class to simplify icon usage
    /// </summary>
    public static class IconManager
    {
        // Commit Icon
        public static IPictureDisp smallCommitPicture = PictureConverter.ImageToPictureDisp(Resources.commit_small);
        public static IPictureDisp largeCommitPicture = PictureConverter.ImageToPictureDisp(Resources.commit_large);

        // Checkout / Pull Icon
        public static IPictureDisp smallCheckoutPicture = PictureConverter.ImageToPictureDisp(Resources.checkOut_small);
        public static IPictureDisp largeCheckoutPicture = PictureConverter.ImageToPictureDisp(Resources.checkOut_large);

        // Lock File Icon
        public static IPictureDisp smallLockFilePicture = PictureConverter.ImageToPictureDisp(Resources.lockFile_small);
        public static IPictureDisp largeLockFilePicture = PictureConverter.ImageToPictureDisp(Resources.lockFile_large);

        // Git Settings Icon
        public static IPictureDisp smallSettingsPicture = PictureConverter.ImageToPictureDisp(Resources.settings_small);
        public static IPictureDisp largeSettingsPicture = PictureConverter.ImageToPictureDisp(Resources.settings_large);

        // Stage Commit Icon
        public static IPictureDisp smallStagePicture = PictureConverter.ImageToPictureDisp(Resources.stage_small);
        public static IPictureDisp largeStagePicture = PictureConverter.ImageToPictureDisp(Resources.stage_large);
    }
}
