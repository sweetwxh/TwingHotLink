using System.Windows.Forms;

/***
 * 作者：吴兴华
 * 日期：2010-12-16
 * Copryright:StepOn Technology
 * */

namespace TwingHotLink
{
    public interface IMainFrame
    {
        void SetMessage(string msg);

        void SetPSPMessage(string msg);

        void ShowMessageBox(string caption, string msg);

        void ShowMessageBox(string caption, string msg, MessageBoxIcon icon);

        void ToggleConnectButton();

        void ToggleBuildButton();

        void AddPlayer(string name);

        void RemovePlayer(string name);

        void RemoveAll();

        void StartCatcher(int requestTimeout);

        void StartServer(string playerId, int maxConnection);

        void StartClient(string playerId, string ipAddress, bool isUdp);

        void StartPing(string ipAddress);

        void SendChat(string msg);

        void RecivedChat(string msg);
    }
}