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

        private ICommand _selectedSectionCommand;
        public ICommand SelectedSectionCommand
        {
            get { return _selectedSectionCommand; }
            set { _selectedSectionCommand = value; ExecuteCommand(value); OnPropertyChanged(); }
        }

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
            SectionCommands.Clear();
            SectionCommands.Add(new VisualCommand("Resumen", "../Resources/tuerca.png", (obj) => CurrentSection = null)); //MOCK
            SectionCommands.Add(new VisualCommand("Usuario", "../Resources/tuerca.png", (obj) => LoadSection<UserConfigViewModel>()));
        }

        #endregion

        #region Private

        private void ExecuteCommand(ICommand value)
        {
            value.Execute(null);
        }

        private void LoadSection<T>()
        {
            CurrentSection = App.IoC.Resolve<T>();
        }

        #endregion
    }
}
