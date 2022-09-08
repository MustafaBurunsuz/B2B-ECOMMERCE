import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ErrorService } from 'src/app/services/error.service';
import { AdminLoginModel } from '../models/admin-login.model';
import { AdminTokenModel } from '../models/admin-token-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  adminTokenModel : AdminTokenModel = new AdminTokenModel();
  constructor(
    @Inject("apiUrl") private apiUrl:string,
    private httpClient :HttpClient,
    private router : Router,
    private toast: ToastrService,
    private errorService: ErrorService
  ) { }

  isAuthenticate(){
    if(localStorage.getItem("adminToken")){
      return true;
    }
    return false;
  }

  login(AdminLoginModel:AdminLoginModel){
    let api = this.apiUrl +"auth/Userlogin";
    this.httpClient.post(api,AdminLoginModel).subscribe((res : any) =>{
      this.adminTokenModel = res.data;
      localStorage.setItem("adminToken",this.adminTokenModel.adminAccessToken);
      this.router.navigate(["/admin"])
      this.toast.success("Giriş başarılı");
    },
    (err)=>{
      this.errorService.errorHandler(err);
    });
  }

  logOut(){
    localStorage.removeItem("adminToken");
    this.router.navigate(["/admin-login"]);
    this.toast.info("Çıkış başarılı");
  }

}
