import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType.';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brandId !== 0) {                                            // must check if not equal to zero bc param initialized from class not from inside component
      params = params.append('brandId', shopParams.brandId.toString());
    }


    if (shopParams.typeId !==0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search)
    }


    params = params.append('sort', shopParams.sort);                          // no need to wrap it in if statement bc sorted by name by default in shopParams
    params = params.append('pageIndex', shopParams.pageNumber.toString());            // adding pagination to our parameters
    params = params.append('pageIndex', shopParams.pageSize.toString());                                


    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
      .pipe(          // method allows to operators to be chained
        map(response => {
          return response.body;
        })

    )
  }

  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
