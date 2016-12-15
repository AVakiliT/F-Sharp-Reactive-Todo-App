# Reactive Todo Application written in C# and F#

This is a demo WPF application inspired by the [Redux.js](http://reduxjs.org) library. It uses a similar approach to 
handling application state.

The Domain Logic (the reducer and actions) are written in F# and is just 12 lines of code and  contains no external dependancies. 
The code is also far shorter than if we had used an external library like [Redux.net]() of [Reducto]().

The GUI code is fairly standard and uses the Prism library for implementing the MVVM pattern.