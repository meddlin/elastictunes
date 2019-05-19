import React, { Component } from 'react';
import { Howl, Howler } from 'howler';
import { Editor } from './Editor/Editor';

import logo from './logo.svg';
import './App.css';

class App extends Component {
    constructor(props) {
        super(props);

        this.playMusic = this.playMusic.bind(this);
        this.pauseMusic = this.pauseMusic.bind(this);
    };

    state = {
        sound: ''
    };

    componentDidMount() {
        var intialHowl = new Howl({
            src: ['http://localhost:53477/api/values/FileDownload'],
            format: ['mp3'],
            autoplay: false,
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

        // Change global volume.
        Howler.volume(0.5);
    }

    playMusic() {
        this.state.sound.play();
    }

    pauseMusic() {
        console.log('paused');
        this.state.sound.pause();
    }

    render() {
    return (
      <div className="App">
        <header className="App-header">Elastic Tunes</header>
        <p className="App-intro">
            <button onClick={this.playMusic}>PLAY</button>
            <button onClick={this.pauseMusic}>PAUSE</button>

        To get started, edit <code>src/App.js</code> and save to reload.
        </p>

        <Editor />
      </div>
    );
  }
}

export default App;
