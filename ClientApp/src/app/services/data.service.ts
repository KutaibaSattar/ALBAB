import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Observer, ReplaySubject, Subject, Subscriber, Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {



  constructor(private baseUrl: string, protected http: HttpClient) {



   }




  // tslint:disable-next-line: typedef
 protected getTableRecords(extraLocation: string ='') {
    return this.http.get(this.baseUrl + extraLocation);

  }

  // tslint:disable-next-line: typedef
  protected createTableRecords(resource,extraLocation: string =''){
    return this.http.post(this.baseUrl+ extraLocation , resource );

  }

  // tslint:disable-next-line: typedef
  protected deleteTableRecord(id){
      return this.http.delete(this.baseUrl + id);
  }

  protected getTableRecordId(id,extraLocation: string ='') : Observable<any>{

   return this.http.get(this.baseUrl + extraLocation + '/' + id);

  }

  protected UpdateTable(resource,extraLocation: string ='') : Observable<any>{

   return this.http.put(this.baseUrl + extraLocation, resource );

  }


  learningObservable(){

/// - cold
/// - creates copy of data
/// - observer can not assign value

    console.log('------------ 🎊 🎉 Lets Start Learning Observables vs Subjects 🎊 🎉 ------------------');

const observable1 = new Observable(a => {
  console.log('Lets initialize observable 1.')
  a.next('Message from Observable 1');
});


const observable2 = new Observable((a : Observer<any>) => {
  console.log('Lets initialize observable 2');
  a.next('Message from Observable 2');
});

// Observer(Subscriber) get data (copy) from observable after subscription

let sub : Subscription = observable2.subscribe(v => console.log(v));

console.log('No message from observable 1 since there is no subscriber/observer.');
console.log('------------ 🙏🏻 END of observable story 🙏🏻 ------------------');


/// - hot
/// - shares data
/// - observer can assign value
/// - subscriber will not receive data streamed before subscription
const subject = new Subject();
let x : Subscription  = subject.subscribe(v => console.log('Observer 1', v)); // initialize observing for 1
let y : void = subject.next('Message 1  from subejct'); // any data inside next sending to observer v = 'Message 1  from subejct'
subject.subscribe(v => console.log('Observer 2', v));
subject.next('Message 2  from subejct');
subject.subscribe(v => console.log('Observer 3', v)); //

console.log('Only observer 1 is able to print all the values brodcated, because he started observing subject before any value brodcasted.');
console.log('------------ 🙏🏻 END of subject story 🙏🏻 ------------------');


/// - hot
/// - shares data
/// - observer can assign value
/// - subscriber will receive data streamed before subscription
const replaySubject = new ReplaySubject();
replaySubject.subscribe(v => console.log('Observer 1', v));

replaySubject.next('Message 1  from replay subejct');
replaySubject.next('Message 2  from replay subejct');

replaySubject.subscribe(v => console.log('Observer 2', v));

console.log('both the observers are able to print all the values brodcated, even though they have subscribed the subject at different of time. you got my point.');
console.log('------------ 🙏🏻 END of Replay Subject story 🙏🏻 ------------------');

/// - hot
/// - shares data
/// - observer can assign value
/// - subscriber will receive data streamed before subscription
/// - can set initial value
const behaviorSubject = new BehaviorSubject('Initial message from Behavior subject');

behaviorSubject.subscribe(v => console.log(v));
behaviorSubject.subscribe(v => console.log(v));


behaviorSubject.next('Message 2 from Behaviroal subject');

console.log('------------🙏🏻  END of Behavioral Subject story 🙏🏻 ------------------');

  }



}
