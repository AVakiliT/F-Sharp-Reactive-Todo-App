using System;
using DomainLogic;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace App
{
    class MainWindowViewModel : BindableBase 
    {
        private Store.Store<Domain.State, Domain.Actions> _store;

        public MainWindowViewModel(Store.Store<Domain.State, Domain.Actions> store)
        {

            _store = store;
            Todos = new ObservableCollection<Domain.Todo>();
            AddTodoCommand = new DelegateCommand(AddTodo);
            _store.StateUpdated += UpdateUI;
        }

        private void UpdateUI(object sender, Domain.State state)
        {
            Todos = new ObservableCollection<Domain.Todo>(state.Todos);
        }

        public ObservableCollection<Domain.Todo> Todos;


        private string _newTodoName;
        public string NewTodName
        {
            get { return _newTodoName; }
            set { SetProperty(ref _newTodoName, value);  }
        }

        public DelegateCommand AddTodoCommand;
        private void AddTodo()
        {
            _store.Dispatch.Invoke(Domain.Actions.NewAddTodo(_newTodoName));
            _newTodoName = "";
        }
    }
}
