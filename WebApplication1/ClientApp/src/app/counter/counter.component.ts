import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-counter-component',
  templateUrl: './counter.component.html'
})
export class CounterComponent {
  public currentCount = 0;

  appUrl: any;
  loginForm: FormGroup;
  submitted: boolean = false;

  constructor(
    private httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private formBuilder: FormBuilder,
    private root: Router
  ) {
    this.appUrl = baseUrl;
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', Validators.required],
      Password: ['', Validators.required]
    });
  }

  async loginAdmin() {
    if (this.loginForm.invalid) {
      this.submitted = true;
      return false;
    }
    else if (this.loginForm.valid) {
      this.httpClient.post(this.appUrl + 'api/Account/Login', this.loginForm.value).subscribe((response: any) => {
        debugger
        if (response["status"] > 0 && response["data"] != null) {

          localStorage.setItem("isLoggedIn", "true");
          localStorage.setItem("userInfo", response["data"]);
          alert(response["message"]);
        }
        else {
          alert(response["message"]);
        }
      })
    }
  }

}
