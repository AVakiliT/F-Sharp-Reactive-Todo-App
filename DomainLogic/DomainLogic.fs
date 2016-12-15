namespace DomainLogic
module Domain = 
    open Store

    type Todo = {Name: string; Complete: bool}
    type TodosFilter = All | InComplete

    type Actions =
    | AddTodo of string
    | ToggleTodo of string
    | ChangeFilter of TodosFilter



    type State = {Todos: Todo list; filter : TodosFilter}

    let reducer (state:State) = function
    | AddTodo name -> {state with Todos = {Name = name; Complete = false }::state.Todos }
    | ToggleTodo name ->  {state with Todos = (state.Todos |> List.map (fun e -> if e.Name = name then {e with Complete = not e.Complete} else e ))}
    | ChangeFilter f -> {state with filter = f}

    let store = Store({Todos = []; filter = All}, reducer)

