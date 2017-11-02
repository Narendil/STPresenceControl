using SugaarSoft.MVVM.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace STPresenceControl.ViewModels
{
    public class MainViewModel : NotificationObject
    {
        #region Binding

        private readonly List<ICommand> _sectionCommands = new List<ICommand>(); //WON'T CHANGE DURING APP LIFETIME
        public List<ICommand> SectionCommands { get { return _sectionCommands; } }

        private object _currentSection;
        public object CurrentSection
        {
            get { return _currentSection; }
            set { _currentSection = value; OnPropertyChanged(); }
        }

        #endregion

        #region Ctor

        public MainViewModel()
        {
            LoadCommands();
        }

        #endregion

        #region Commands

        private void LoadCommands()
        {  
        }

        #endregion
    }
}
