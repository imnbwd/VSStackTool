using System;

namespace StackTool
{
    public class Constant
    {
        #region Guids

        public const string PackageCommandSetGuidString = "8f66deb0-240b-4137-8550-723344e49722";

        public const string PackageGuidString = "5498f07e-2ca1-4d3b-bcfb-5e8a8a082eed";

        #endregion Guids

        #region CommandIds

        public const int UwpCommandId = 0x2001;
        public const int XamlCommandId = 0x2002;
        public const int WpfCommandId = 0x2003;
        public const int GithubCommandId = 0x2004;

        public const int GenerateNPropertyCommandId = 0x2101;
        public const int BingCommandId = 0x2005;
        public const int DocsCommandId = 0x2006;

        #endregion CommandIds

        public static Guid PackageCommandSetGuid
        {
            get
            {
                return new Guid(PackageCommandSetGuidString);
            }
        }
    }
}