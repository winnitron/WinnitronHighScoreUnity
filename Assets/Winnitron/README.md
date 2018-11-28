# Winnitron High Score Unity Client

`v1.0-alpha`

## Usage

- Attach a `Scoreboard` component to a `GameObject`.
- Set its API Key and Secret. You can get these values from your game's page on [The Winnitron Network](http://network.winnitron.com/).

### Sending a high score

```c#
HighScore score = new HighScore("Jim", 1000);
scoreboard.Send(score, SuccessHandler);

public object SuccessHandler(object created) {
  // created is an instance of HighScore
  // The world is your oyster.
}
```

### Fetching the existing high score list

```c#
// top five scores on the Winnitron being played RIGHT NOW
scoreboard.Get(5, Scoreboard.THIS_WINNITRON, SuccessHandler);

// top ten scores across the entire Winnitron Network
scoreboard.Get(10, Scoreboard.ALL_WINNITRONS, SuccessHandler);

// top twenty scores on the Winnitron 1000
scoreboard.Get(20, "winnitron-1000", SuccessHandler);

public object SuccessHandler(object results) {
  HighScore[] scoreList = (HighScore[]) results;
  // ...
}
```

### Test Sandbox

Fetching high scores in test mode will only return scores that were also created in test mode.

## Contributing

https://github.com/winnitron/WinnitronDummyGame

## Oh god! Send help!

All the myriad ways you can find us on the information superhighway:

http://network.winnitron.com/contact/
