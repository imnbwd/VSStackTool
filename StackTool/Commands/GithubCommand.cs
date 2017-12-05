using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace StackTool.Commands
{
    public sealed class GithubCommand
    {
        private readonly System.IServiceProvider package;
        private GithubCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var githubCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.GithubCommandId);
                var githubMenuItem = new MenuCommand(MenuItemClick, githubCommandId);
                commandService.AddCommand(githubMenuItem);
            }
        }

        public static GithubCommand Instance { get; private set; }
        private System.IServiceProvider ServiceProvider => package;
        public static void Initialize(Package package)
        {
            Instance = new GithubCommand(package);
        }

        private void MenuItemClick(object sender, EventArgs e)
        {
            Process.Start("http://github.com/imnbwd");
        }
    }
}