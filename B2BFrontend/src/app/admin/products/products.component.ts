import { Component, OnInit } from '@angular/core';
import { ErrorService } from 'src/app/services/error.service';
import { ProductModel } from './model/product.model';
import { ProductService } from './service/product.service';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  products: ProductModel[] = [];
  constructor(

    private errorService:ErrorService,
    private productService:ProductService
  ) { }

  ngOnInit(): void {
    this.getList();
  }
  getList(){
    this.productService.getList().subscribe((res:any)=>{
      this.products = res.data;
      console.log(this.products);
    },
    (err)=>{
      this.errorService.errorHandler(err);
    });
  }

}
