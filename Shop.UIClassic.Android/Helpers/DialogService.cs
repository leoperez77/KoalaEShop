
using Android.App;
using Android.Content;

namespace Shop.UIClassic.Android.Helpers
{
    public static class DialogService
    {
        public static void ShowMessage(Context context, string title, string message, string button)
        {
            new AlertDialog.Builder(context)
                .SetPositiveButton(button, (sent, args) => { })
                .SetMessage(message)
                .SetTitle(title)
                .SetCancelable(false)
                .Show();
        }

    }
}