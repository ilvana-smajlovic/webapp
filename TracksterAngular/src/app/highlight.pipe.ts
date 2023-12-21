import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'highlight'
})
export class HighlightPipe implements PipeTransform {
  transform(text: string, search: string, hoveredMedia: any): string {
    console.log('search:', search);
    console.log('hoveredMedia:', hoveredMedia);
    if (!search || !hoveredMedia || text !== hoveredMedia.name) {
      return text;
    }

    const pattern = new RegExp(search, 'gi');
    return text.replace(pattern, (match) => `<span class="highlight">${match}</span>`);
  }
}
