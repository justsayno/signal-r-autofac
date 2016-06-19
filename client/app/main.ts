import {bootstrap} from '@angular/platform-browser-dynamic';
import {provide} from "@angular/core";
import {HTTP_PROVIDERS} from "@angular/http";

import "rxjs/add/operator/map";

import {AppComponent} from './app.component';

import {ChannelService, ChannelConfig, SignalrWindow} from "./services/channel.service";


let channelConfig = new ChannelConfig();
channelConfig.url = "http://localhost:9123/signalr";
channelConfig.hubName = "EventHub";

bootstrap(AppComponent, [
    HTTP_PROVIDERS,
    ChannelService,
    provide(SignalrWindow, {useValue: window}),
    provide("channel.config", { useValue: channelConfig })
]);
