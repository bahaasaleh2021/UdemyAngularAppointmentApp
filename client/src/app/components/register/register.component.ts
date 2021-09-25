import { Component, OnInit,EventEmitter, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

@Output() cancel=new EventEmitter();
registerForm:FormGroup;
maxDate:Date;
validationErros:string[]=[];

  constructor(public accountSerivce:AccountService,private toastr :ToastrService,private router:Router
    ,private fb:FormBuilder) { }

  ngOnInit(): void {
    this.initalizeForm();
    this.maxDate=new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);
  }

  initalizeForm(){
    this.registerForm=this.fb.group({
      gender:['male'],
      username:['',Validators.required],
      knownAs:['',Validators.required],
      dateOfBirth:['',Validators.required],
      city:['',Validators.required],
      country:['',Validators.required],
      password:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      confirmPassword:['',[Validators.required,this.matchValuesValidator('password')]]
    });

    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  matchValuesValidator(matchAgainst:string):ValidatorFn{
    return (control:AbstractControl)=>{
        return control?.value===control?.parent?.controls[matchAgainst]?.value?null:{notMatching:true};
    }
  }

  register(){
    
    
    this.accountSerivce.register(this.registerForm.value).subscribe(res=>{
      if(res!=null){
        this.router.navigateByUrl('/members');

      }
      
    },
    error=>
    {
       this.validationErros=error;
    } );
  }

  cancelRegister(){
    this.cancel.emit(false);
  }





}
