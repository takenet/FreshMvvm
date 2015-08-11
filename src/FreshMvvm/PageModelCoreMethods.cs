using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FreshMvvm
{
    public class PageModelCoreMethods : IPageModelCoreMethods
    {
        Page _currentPage;
        FreshBasePageModel _pageModel;

        public PageModelCoreMethods(Page currentPage, FreshBasePageModel pageModel)
        {
            _currentPage = currentPage;
            _pageModel = pageModel;
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            if (_currentPage != null)
                await _currentPage.DisplayAlert(title, message, cancel);
        }

        public async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            if (_currentPage != null)
                return await _currentPage.DisplayActionSheet(title, cancel, destruction, buttons);
            return null;
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            if (_currentPage != null)
                return await _currentPage.DisplayAlert(title, message, accept, cancel);
            return false;
        }

        public async Task PushPageModel<T>(T pageModel, object data, bool modal = false, bool animated = true) where T : FreshBasePageModel
        {
            var page = FreshPageModelResolver.ResolvePageModel<T>(data, pageModel);

            pageModel.PreviousPageModel = _pageModel;

            IFreshNavigationService rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>();

            await rootNavigation.PushPage(page, pageModel, modal, animated);
        }

        public async Task PushPageModel<T>(object data, bool modal = false, bool animated = true) where T : FreshBasePageModel
        {
            T pageModel = FreshIOC.Container.Resolve<T>();
            await PushPageModel<T>(pageModel, data, modal, animated);
          
        }

        public async Task PopPageModel(bool modal = false, bool animated = true)
        {
            IFreshNavigationService rootNavigation = FreshIOC.Container.Resolve<IFreshNavigationService>();
            await rootNavigation.PopPage(modal, animated);
        }

        public async Task PopPageModel(object data, bool modal = false, bool animated = true)
        {
            if (_pageModel != null && _pageModel.PreviousPageModel != null && data != null)
            {
                _pageModel.PreviousPageModel.ReverseInit(data);
            }
            await PopPageModel(modal, animated);
        }

        public Task PushPageModel<T>(bool animated = true) where T : FreshBasePageModel
        {
            return PushPageModel<T>(null, false, animated);
        }

        public void BatchBegin()
        {
            _currentPage.BatchBegin();
        }

        public void BatchCommit()
        {
            _currentPage.BatchCommit();
        }


        public async Task PopToRoot(object data = null, bool modal = false, bool animated = true)
        {
            if (_currentPage != null)
            {
                await _currentPage.Navigation.PopToRootAsync();
            }
        }

        public async Task ReplaceCurrentPageModel<T>(object data = null, bool animated = true) where T : FreshBasePageModel
        {
            T pageModel = FreshIOC.Container.Resolve<T>();            
            await PushPageModel<T>(pageModel, data, false, animated);
            pageModel.PreviousPageModel = _pageModel.PreviousPageModel;
            _currentPage.Navigation.RemovePage(_currentPage);
        }
    }
}

