﻿using DevExpress.EasyTest.Framework;
using DevExpress.EasyTest.Framework.Commands;

namespace Xpand.TestsLib.EasyTest.Commands{
    public class NavigateCommand:EasyTestCommand{
        public const string Name = "Navigate";

        public NavigateCommand(string navigationItemCaption){
            Parameters.MainParameter=new MainParameter(navigationItemCaption);
        }

        protected override void ExecuteCore(ICommandAdapter adapter){
            var actionCommand = new DevExpress.EasyTest.Framework.Commands.ActionCommand();
            var parameterList = actionCommand.Parameters;
            parameterList.MainParameter=new MainParameter("Navigation");
            parameterList.ExtraParameter = Parameters.MainParameter;
            actionCommand.Execute(adapter);

        }
    }
}