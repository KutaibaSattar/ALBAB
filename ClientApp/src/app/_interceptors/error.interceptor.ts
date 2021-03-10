import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError} from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor( private router: Router, private toastr: ToastrService) {} // router for roue to errors page

  // next is a response (in), request is (out)
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    /*
    http.get('https://test.com/').pipe(
    tap(
        () => {
            // 200, awesome!, no errors will trigger it.
        },
        () => {
            // error is here, but we can only call side things.
        },
    ),
    catchError(
        (error: HttpErrorResponse): Observable<any> => {
            // we expect 404, it's not a failure for us.
            if (error.status === 404) {
                return of(null); // or any other stream like of('') etc.
            }

            // other errors we don't know how to handle and throw them further.
            return throwError(error);
        },
    ),
).subscribe(
    response => {
        // 200 triggers it with proper response.
        // 404 triggers it with null. `tap` can't make 404 valid again.
    },
    error => {
        // any error except 404 will be here.
    },
);
    */



    return next.handle(request).pipe(
      catchError( (errorResponse : HttpErrorResponse) => {  // maping catch error , errorResponse is response of error because it is inside catchError
        if (errorResponse) {

          switch (errorResponse.status) {
            case 400: // bad request
              if (errorResponse.error.errors != undefined)
                {

                  if (errorResponse.error.errors) {
                    const modalStateError = [];
                    for (const key in errorResponse.error.errors) {
                      if (errorResponse.error.errors[key]) {
                        modalStateError.push(errorResponse.error.errors[key]);
                        this.toastr.error(errorResponse.error.errors[key]);
                      }
                      //console.log(modalStateError.flat())
                      throw modalStateError.flat();
                    }

                  }
                  else
                  {

                  }
              }




              else {
                this.toastr.error(errorResponse.error ,errorResponse.status.toFixed());

              }
            break;

            case 401: // auth
            this.toastr.error('UnAuthorized' ,errorResponse.status.toFixed());
              break;
            case 404: // not found
              this.toastr.error('Not found' ,errorResponse.status.toFixed());
              this.router.navigateByUrl('/not-found');
              break;
            case 500: // server inetrnal error
              const navigationExtras: NavigationExtras = { state: { error: errorResponse.error } };
              this.router.navigateByUrl('/server-error', navigationExtras);
              break;
            default:
            this.toastr.error('Something unexpected went wrong');
            console.log(errorResponse);
            break;
          }

        }
        return throwError(errorResponse.error); // if not catching throwError errorResponse

      })

    );
  }
}
