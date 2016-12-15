using System;
using DomainLogic;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp
{
    class MainWindowViewModel : BindableBase
    {
        private Store.Store<Domain.State, Domain.Actions> _store;

        public MainWindowViewModel(Store.Store<Domain.State, Domain.Actions> store)
        {
            _store = store;

            AddTodoCommand = new DelegateCommand(AddTodo, AddTodoCanExecute);
            ToggleCompleteCommand = new DelegateCommand<Domain.Todo>(ToggleComplete);
            ToggleFilterCommand = new DelegateCommand<string>(ToggleFilter);

            _store.StateUpdated += UpdateUI;
        }


        //Misc

        private void UpdateUI(object sender, Domain.State state)
        {
            if (state.filter == Domain.TodosFilter.All)
                Todos = state.Todos.ToList();
            else
                Todos = state.Todos.Where(todo => todo.Complete == false).ToList();

        }

        // Props

        private List<Domain.Todo> _todos = new List<Domain.Todo>();
        public List<Domain.Todo> Todos
        {
            get { return _todos; }
            set { SetProperty(ref _todos, value); }
        }

        private string _newTodoName;
        public string NewTodoName
        {
            get { return _newTodoName; }
            set {
                SetProperty(ref _newTodoName, value);
                AddTodoCommand.RaiseCanExecuteChanged();
            }
        }


        // Commands

        public DelegateCommand AddTodoCommand { get; set; }
        private void AddTodo()
        {
            _store.Dispatch.Invoke(Domain.Actions.NewAddTodo(NewTodoName));
            NewTodoName = "";
        }
        private bool AddTodoCanExecute()
        {
            return !Todos.Any(todo => todo.Name == NewTodoName) & !string.IsNullOrWhiteSpace(NewTodoName);
        }

        public DelegateCommand<string> ToggleFilterCommand { get; set; }
        private void ToggleFilter(string filter)
        {
            switch (filter)
            {
                case "All":
                    _store.Dispatch.Invoke(Domain.Actions.NewChangeFilter(Domain.TodosFilter.All));
                    break;
                case "InComplete":
                    _store.Dispatch.Invoke(Domain.Actions.NewChangeFilter(Domain.TodosFilter.InComplete));
                    break;
            }
        }

        public DelegateCommand<Domain.Todo> ToggleCompleteCommand { get; set; }
        private void ToggleComplete(Domain.Todo todo)
        {
            _store.Dispatch.Invoke(Domain.Actions.NewToggleTodo(todo.Name));
        }
    }
}
