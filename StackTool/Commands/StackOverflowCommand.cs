using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace StackTool.Commands
{
    public class StackOverflowCommand
    {
        private readonly Package package;

        private StackOverflowCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var uwpCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.UwpCommandId);
                var uwpCommandItem = new MenuCommand(MenuItemClick, uwpCommandId);
                commandService.AddCommand(uwpCommandItem);

                var xamlCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.XamlCommandId);
                var xamlCommandItem = new MenuCommand(MenuItemClick, xamlCommandId);
                commandService.AddCommand(xamlCommandItem);

                var wpfCommandId = new CommandID(Constant.PackageCommandSetGuid, Constant.WpfCommandId);
                var wpfCommandItem = new MenuCommand(MenuItemClick, wpfCommandId);
                commandService.AddCommand(wpfCommandItem);
            }
        }

        public static StackOverflowCommand Instance { get; private set; }
        protected IServiceProvider ServiceProvider => package;

        public static void Initialize(Package package)
        {
            Instance = new StackOverflowCommand(package);
        }

        protected void MenuItemClick(object sender, EventArgs e)
        {
            var cmd = sender as MenuCommand;
            if (cmd != null && cmd.CommandID != null)
            {
                string url = "http://stackoverflow.com/questions/tagged/{0}";
                switch (cmd.CommandID.ID)
                {
                    case Constant.UwpCommandId:
                        url = string.Format(url, "uwp");
                        break;

                    case Constant.XamlCommandId:
                        url = string.Format(url, "xaml");
                        break;

                    case Constant.WpfCommandId:
                        url = string.Format(url, "wpf");
                        break;
                }

                Process.Start(url);
            }
        }
    }
}