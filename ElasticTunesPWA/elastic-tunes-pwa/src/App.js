import React, { Component } from 'react';
import { Howl, Howler } from 'howler';

import logo from './logo.svg';
import './App.css';

class App extends Component {
    constructor(props) {
        super(props);

        this.playMusic = this.playMusic.bind(this);
    };

    state = {
        sound: ''
    };

    playMusic() {
        alert('playing...');
        this.state.sound.play();
    }

    componentDidMount() {
        var intialHowl = new Howl({
            src: ['http://localhost:53477/api/values/FileDownload'],
            format: ['mp3'],
            autoplay: true,
            html5: true,
            loop: true,
            mobileAutoEnable: true,
            onend: function () {
                console.log('finished!');
            },
            onplayerror: function () {
                console.log('unable to play');
            }
        });

        this.setState({ sound: intialHowl });

        intialHowl.play();

        // Change global volume.
        Howler.volume(0.5);
    }

    render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <h1 className="App-title">Welcome to React</h1>
        </header>
            <p className="App-intro">
                <button onClick={this.playMusic}>PLAY</button>

          To get started, edit <code>src/App.js</code> and save to reload.
        </p>
      </div>
    );
  }
}

export default App;
