import { Directive, Input, ElementRef, HostListener, Renderer2, Renderer} from '@angular/core';

@Directive({
  selector: '[appSelector]'
})
export class SelectorDirective {

  @Input() mouseEnterBgColor : string;
  @Input() mouseOverBgColor : string;
  @Input() mouseLeaveBgColor : string;
  @Input() onClickBgColor : string;

  @Input() mouseEnterColor : string;
  @Input() mouseOverColor : string;
  @Input() mouseLeaveColor : string;
  @Input() onClickColor : string;

  constructor(
    private elementRef : ElementRef
  ) { }

  /** @HostListener('mouseenter') onMouseEnter() {
    this.highlight(this.mouseEnterBgColor, this.mouseEnterColor);
  }

  @HostListener('mouseleave') onMouseLeave() {
    this.highlight(this.mouseLeaveBgColor, this.mouseLeaveColor);
  }

  @HostListener('mouseover') onMouseOver() {
    this.highlight(this.mouseOverBgColor, this.mouseOverColor);
  }
*/
  @HostListener('click') onClickChange() {
  }


  highlight(bgColor : string, color : string){
    this.elementRef.nativeElement.style.backgroundColor = bgColor;
    this.elementRef.nativeElement.style.color = color;
  }
}
