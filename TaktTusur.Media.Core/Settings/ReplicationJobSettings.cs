namespace TaktTusur.Media.Core.Settings;

public class ReplicationJobSettings : JobSettingsBase
{
	public int MaxReplicatedItems { get; set; }
	
	public int CommitBuffer { get; set; }
}