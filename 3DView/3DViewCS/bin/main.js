// NOTE! This code is for demonstration purposes only and does not contain any kind of
// 		 error handling. MUST be revised before using in production.

"use strict";

function OnNewShellUI(shellUI)
{
	/// <summary>Executed by the UIX when a ShellUI module is started.</summary>
	/// <param name="shellUI" type="MFiles.ShellUI">The shell UI object which was created.</param>

	// This is the start point of a ShellUI module.
	
	// Register to be notified when a new normal shell frame (Event_NewNormalShellFrame) is created.
	// We use Event_NewNormalShellFrame rather than Event_NewShellFrame as this won't fire for history (etc.) dialogs.
	// ref: https://www.m-files.com/UI_Extensibility_Framework/index.html#Event_NewNormalShellFrame.html
	shellUI.Events.Register(
		Event_NewNormalShellFrame,
		handleNewNormalShellFrame );
}

function handleNewNormalShellFrame(shellFrame)
{
	/// <summary>Handles the OnNewNormalShellFrame event for an IShellUI.</summary>
	/// <param name="shellFrame" type="MFiles.ShellFrame">The shell frame object which was created.</param>

	// The shell frame was created but it cannot be used yet.
	// The following line would throw an exception ("The object cannot be accessed, because it is not ready."):
	// shellFrame.ShowMessage("A shell frame was created");

	// Register to be notified when the shell frame is started.
	shellFrame.Events.Register(
		Event_Started,
		getShellFrameStartedHandler( shellFrame ) );
}

function getShellFrameStartedHandler(shellFrame)
{
	/// <summary>Returns a function which handles the OnStarted event for an IShellFrame.</summary>

	// The shell frame is now started and can be used.

	return function()
	{
		var run3DViewCommand = shellFrame.Commands.CreateCustomCommand( "Run 3DView" );
		// Add the first command to the task area.
		shellFrame.TaskPane.AddCustomCommandToGroup( run3DViewCommand, TaskPaneGroup_Main, 1 );
		// Add the first command to the context menu.
		shellFrame.Commands.AddCustomCommandToMenu( run3DViewCommand, MenuLocation_ContextMenu_Top, 1 );
		// Register to be notified when a custom command is clicked.
		// Note: this will fire for ALL custom commands, so we need to filter out others.
		shellFrame.Commands.Events.Register(
			Event_CustomCommand,
			function(commandId)
			{
				// Branch depending on the Id of the command that was clicked.
				switch (commandId)
				{
					case run3DViewCommand:
						var connectionNames = shellFrame.ShellUI.MFilesClientApplication.GetVaultConnections().Item[0].Name;
						var info = "NULL";
						var count = shellFrame.Listing.CurrentSelection.GetObjectVersionsCount();
						if (count != 0) {
							var objectVersions = shellFrame.Listing.CurrentSelection.ObjectVersions;
							info = "";
							for (var i = 0; i < count; i++) {
								if (i > 0) {
									info = info + "%";
								}
								info = info +
									objectVersions[i].ObjVer.Type + "?" +
									objectVersions[i].ObjVer.ID + "?" +
									objectVersions[i].ObjVer.Version;// + "?" +
										//objectVersions[i].Title;
								// Type?ID?Version%Type?ID?Version
							}
						}
						
						// Open the application.
						var shell = new ActiveXObject( "WScript.Shell" );
						shell.Run(MFiles.ApplicationPath + "\\Release\\3DViewCS.exe " +
							MFiles.ApplicationPath + " " +
							info + " " +
							shellFrame.ShellUI.Vault.SessionInfo.VaultGUID,
							connectionNames,
							1);
						break;
				}
			} 
		);
	}
}