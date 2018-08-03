namespace WebSharper.React

open WebSharper
open WebSharper.JavaScript

open WebSharper.React.Bindings

module Resources =
    
    type React = Resources.React

[<JavaScript>]
module React =
    
    type private Window with
        static member Current with [<Inline "window">] get () = X<Window>

        [<Inline "$0.addEventListener($1, $2)">]
        member this.AddEventListener (_ : string, _ : (unit -> unit)) = ()

    let Class initialState renderer =
        {
            InitialState = initialState
            Renderer     = renderer
            Events       = []
            Context      = None
        }

    let Mount target (component' : Component) =
        React.Render(component'.Map(), target)

    let Router<'a, 'b when 'a : equality and 'b :> Component> routeMap (renderer : Router<'a> -> 'b) : Class<'a, 'b> =
        let action =
            Window.Current.LocalStorage.GetItem "last-action"
            |> (fun action ->
                if action = null then
                    routeMap.Deserializer []
                else
                    As<'a> (JSON.Parse action)
            )

        Class action
        <| fun this ->
            let url =
                routeMap.Serializer this.State
                |> (fun parts ->
                    if List.isEmpty parts then
                        "/"
                    else
                        List.fold (fun state -> (+) (state + "/")) "" parts
                )

            Window.Current.Location.Hash <- "#" + url

            renderer this
        |> OnUpdate (fun this _ ->
            Window.Current.LocalStorage.SetItem("last-action", JSON.Stringify this.State)
        )
        |> OnMount (fun class' _ ->
            Window.Current.AddEventListener("hashchange", fun () ->
                let newState =
                    (Window.Current.Location.Hash.Substring 2).Split('/')
                    |> (fun parts ->
                        if Array.isEmpty parts then
                            []
                        else
                            Array.toList parts
                    )
                    |> routeMap.Deserializer

                if class'.State <> newState then
                    class'.SetState newState
            )
        )
