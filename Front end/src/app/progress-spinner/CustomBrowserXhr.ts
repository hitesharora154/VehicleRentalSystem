
// import { Observable } from "rxjs/Observable";
// import { Subscriber } from "rxjs/Subscriber";
// import { BrowserXhr, XhrEvent } from "@angular/http";

// export class CustomBrowserXhr extends BrowserXhr {

//     private _observable: Observable<XhrEvent>;
//     private _subscriber: Subscriber<XhrEvent>;

//     constructor() {
//         super();
//         this._observable = Observable.create(subscriber => {
//             this._subscriber = subscriber;
//         }).share();
//     }


//     get observable(): Observable<XhrEvent> {
//         return this._observable;
//     }

//     build(): any {
//         let xhr = super.build();
//         if (!this._subscriber) return xhr;

//         //at the beginning, we create an event that notifies an opening of a connection
//         this._subscriber.next({ type: 'open', event: {} });

//         xhr.onprogress = (event) => {
//             this._subscriber.next({ type: 'progress', event: event });
//         };
//         xhr.onload = (event) => {
//             this._subscriber.next({ type: 'load', event: event });
//         };
//         xhr.onerror = (event) => {
//             this._subscriber.next({ type: 'error', event: event });
//         };
//         xhr.onabort = (event) => {
//             this._subscriber.next({ type: 'abort', event: event });
//         };

//         return xhr;
//     }

// }