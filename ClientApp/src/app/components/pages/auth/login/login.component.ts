import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  formBuilder: FormBuilder;
  httpClient: HttpClient;
  router: Router;
  public phoneNumber: FormControl | undefined;
  public password: FormControl | undefined;

  public constructor(httpClient: HttpClient, formBuilder: FormBuilder,
    router: Router) {
    this.formBuilder = formBuilder;
    this.httpClient = httpClient;
    this.router = router;

    this.phoneNumber = new FormControl('', Validators.required);
    this.password = new FormControl('', Validators.required);

    this.loginForm = this.formBuilder.group({
      phoneNumber: this.phoneNumber,
      password: this.password
    });
  }

  public loginForm: FormGroup;

  public login() {
    if (this.loginForm?.valid) {
      this.httpClient.post('http://localhost:5284/api/Auth/login', this.loginForm?.value).subscribe((response) => {
        var json = JSON.stringify(response);
        localStorage.setItem('userData', json);
        this.router.navigate(['/home']);
      });
    }
  }
}
