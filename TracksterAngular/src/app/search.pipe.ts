import { Pipe, PipeTransform } from '@angular/core';
import {Media} from "./models/media";

@Pipe({ name: 'appFilter' })

export class SearchPipe implements PipeTransform {
  /**
   * Pipe filters the list of elements based on the search text provided
   *
   * @param items list of elements to search in
   * @param searchText search string
   * @returns list of elements filtered by search text or []
   */
  transform(list: any[], searchText: string): any[] {
    return list ? list.filter(item => item.name.search(new RegExp(searchText))) : [];
  }
}
