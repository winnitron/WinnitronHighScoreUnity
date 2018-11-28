# Winnitron High Score Unity Client

`v1.0-alpha`

## Usage

- Attach a `Scoreboard` component to a `GameObject`.
- Set its API Key and Secret. You can get these values from your game's page on [The Winnitron Network](http://network.winnitron.com/).

### Sending a high score

```c#

HighScore score = new HighScore("Jim", 1000);
scoreboard.Send(score, SuccessHandler);

public object SuccessHandler(HighScore created) {
  // The world is your oyster.
}
```

### Fetching the existing high score list

```c#
scoreboard.Get(10, SuccessHandler)

public object SuccessHandler(HighScore[] scoreList) {
  // ...
}
```

## Contributing

https://github.com/winnitron/WinnitronDummyGame

## Oh god! Send help!

All the myriad ways you can find us on the information superhighway:

http://network.winnitron.com/contact/
