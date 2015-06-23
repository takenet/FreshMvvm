using System;
using System.Threading.Tasks;

namespace FreshMvvm
{
    public interface IPageModelCoreMethods
    {
        Task DisplayAlert (string title, string message, string cancel);

        Task<string> DisplayActionSheet (string title, string cancel, string destruction, params string[] buttons);

        Task<bool> DisplayAlert (string title, string message, string accept, string cancel);

        Task PushPageModel<T> (object data, bool modal = false, bool animated = true) where T : FreshBasePageModel;

        Task PopPageModel (bool modal = false, bool animated = true);

        Task PopPageModel (object data, bool modal = false, bool animated = true);

        Task PushPageModel<T> (bool animated = true) where T : FreshBasePageModel;

        Task PopToRoot(object data = null, bool modal = false, bool animated = true);

		void BatchBegin();

		void BatchCommit();
    }
}

