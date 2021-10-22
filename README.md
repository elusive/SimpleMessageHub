# SimpleMessageHub
![main](https://github.com/elusive/SimpleMessageHub/actions/workflows/dotnet.yml/badge.svg?branch=main)

Super simple and low volume message bus for smaller apps and utilities. Does not include automatic unsubscribing 
or use of weak references. If you expect a high volume of unsubscribed messages then you may wish to use a more 
mature library.

## Overview
This is a simple messagebus class written in C# for dotnet 5.0. The class uses in internal dictionary to track
observers of a message by its CLR type. Each entry in the dictionary is a list of subscription objects that are
made up of a subscription token [`Guid`] and the action to execute [`Action<TMessage>`].

The main interface is `IMessageBus` which covers the following general use cases:
1. Subscribing to a type of message with a delegate handler that accepts the message as its parameter.
2. Publishing a message to all subscribers.
3. Unsubscribing from a message type using the subscription token returned from the subscribe call.

## Usage

#### Subscribe
The following syntax can be used to subscribe to a message. Assuming there is an existing message class: `TestMessage : IMessage`

```csharp
var _messageBus = MessageBus.Default;
var handledCount = 0;
var token = _messageBus.Subscribe<TestMessage>(msg => handledCount++);
```

#### Publish
Continuing with our `TestMessage` class example we can publish the message in order to have the handlers executed.

```csharp
var _messageBus = MessageBus.Default;
var msg1 = new TestMessage { Message = "Some message content." };
_messageHub.Publish(msg1)
```

#### Unsubscribe
You then should unsubscribe to the message when no longer concerned with its publishing. Unsubscribing is not automatic.

```csharp
var _messageBus = MessageBus.Default;
_messageBus.Unsubscribe(token);   // token returned from sub call
```
