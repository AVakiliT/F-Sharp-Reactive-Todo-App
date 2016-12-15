module  Store

type Store<'State, 'Action>(initialState, reducer) = 
    let updateEvent = Event<_>()
    let mutable state : 'State = initialState
    let dispatch (action:'Action) =
        state <- reducer state action
        updateEvent.Trigger state
    member x.Dispatch = dispatch
    [<CLIEvent>]
    member x.StateUpdated = updateEvent.Publish 
