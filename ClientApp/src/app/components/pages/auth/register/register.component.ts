import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterModule
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  formBuilder: FormBuilder;
  httpClient: HttpClient;
  router: Router;
  public fullName: FormControl | undefined;
  public phoneNumber: FormControl | undefined;
  public password: FormControl | undefined;
  public confirmPassword: FormControl | undefined;

  public constructor(httpClient: HttpClient, formBuilder: FormBuilder,
    router: Router) {
    this.formBuilder = formBuilder;
    this.httpClient = httpClient;
    this.router = router;

    this.fullName = new FormControl('', Validators.required);
    this.phoneNumber = new FormControl('', Validators.required);
    this.password = new FormControl('', Validators.required);
    this.confirmPassword = new FormControl('', Validators.required);



    this.registerForm = this.formBuilder.group({
      fullName: this.fullName,
      phoneNumber: this.phoneNumber,
      password: this.password
    });
  }

  public registerForm: FormGroup;


  public isValidConfirmPassword(): boolean {
    return this.password?.value === this.confirmPassword?.value;
  }

  public isValidForm() {
    return this.registerForm?.valid && this.isValidConfirmPassword();
  }

  public register() {
    if (this.isValidForm()) {
      this.httpClient.post('http://localhost:5284/api/Auth/register', this.registerForm?.value).subscribe((response) => {
        this.router.navigate(['/login']);
      });
    }
  }
}
