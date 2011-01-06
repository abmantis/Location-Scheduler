using Microsoft.WindowsCE.Forms;
using System.Text;
using System;
using HelperLib;

public class MessageNotifWindow : MessageWindow
{
	public MessageNotifWindow()
	{
		this.Text = "CoreMsgNotifReciver";
	}

	protected override void WndProc(ref Message msg)
	{
		switch (msg.Msg)
		{
			case NotifMessages.NOTIF_START:
				Console.WriteLine("Mensagem!");
				break;
		}

		// call the base class WndProc for default message handling
		base.WndProc(ref msg);
	}
}