import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor() {}
    // HttpInterceptor with request
  /*   next : next interceptor in que otherwise
  it represents HttpXhrBackend in case if there are no more interceptors. */
  /* The return type of intercept is Observable<HttpEvent<any>>;
  which means that, the request is going to produce a series of HttpEvents,
  such as HttpHeaderResponse, HttpResponse HttpProgressEvent etc.
  The (any) represents the response type,which means that we can receive any
  type of data as response; not only get array. */
  /*  The "request" parameter represents the current request.
   But the limitation is, we cannot modify the current request.
   That means we cannot add any headers in the current request.
    So we have to clone the request that means we are creating a duplicate request; */
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    //var currentUser = { token: '' };
    if (sessionStorage.currentUser != null) {
      //currentUser = JSON.parse(sessionStorage.currentUser);
      request = request.clone({
        setHeaders: {
          Authorization: 'Bearer ' + sessionStorage.currentUser,
        },
      });
    }
   /*  So we have duplicated the request.
    And also added a request header called "Authorization". */

    /* Now we invoke the next interceptor, if any.
       Otherwise we have to invoke the HttpXhrBackend, if there are no more interceptors.
       The next.handleRequest invokes the next interceptor, if any;
       otherwise it invokes the HttpXhrBackend.
      That means then the actual request will be sent to the server. */

    return next.handle(request); // because we changed the request by clone it so we send again by next
  }
}
