import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { RouterModule, Routes } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';
import { NavbarModule } from './navbar/navbar.module';
import { AsideModule } from './aside/aside.module';
import { FooterModule } from './footer/footer.module';

const routes: Routes = [

  {path: '',
   component: LayoutComponent
  }
]

@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NavbarModule,
    AsideModule,
    FooterModule
  ],
  exports: [
    LayoutComponent
  ]
})
export class LayoutsModule { }
