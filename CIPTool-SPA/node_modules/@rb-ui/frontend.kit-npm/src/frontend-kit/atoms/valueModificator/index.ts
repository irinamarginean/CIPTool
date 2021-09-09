import BaseComponent from '../../baseComponent';

const MINUS_ICON = 'a-value-modificator__minus-icon';
const PLUS_ICON = 'a-value-modificator__plus-icon';
const DISABLED = 'a-value-modificator__icon-disabled';

const PRESSED = '-pressed';

export default class ValueModificator extends BaseComponent {
  protected static events = ['onClick'];

  private readonly minusIcon: HTMLElement;

  private readonly plusIcon: HTMLElement;

  private readonly input: HTMLInputElement;

  constructor(container: HTMLElement) {
    super(container);

    /**
     * Define DOM Elements and Variables
     */
    this.minusIcon = container.querySelector(`.${MINUS_ICON}`);
    this.plusIcon = container.querySelector(`.${PLUS_ICON}`);
    this.input = container.querySelector('input');

    if (this.input instanceof HTMLInputElement) {
      if (!this.input.value) {
        this.input.value = '0';
      }
    }

    if (this.minusIcon instanceof HTMLElement) {
      this.initElement(this.minusIcon);

      this.minusIcon.onclick = (): void => {
        this.stepDownInputValue();
      };
      this.minusIcon.addEventListener('mousedown', () => {
        this.togglePressed(this.minusIcon);
      });
      this.minusIcon.addEventListener('mouseup', () => {
        this.togglePressed(this.minusIcon);
      });
    }

    if (this.plusIcon instanceof HTMLElement) {
      this.initElement(this.plusIcon);

      this.plusIcon.onclick = (): void => {
        this.stepUpInputValue();
      };
      this.plusIcon.addEventListener('mousedown', () => {
        this.togglePressed(this.plusIcon);
      });
      this.plusIcon.addEventListener('mouseup', () => {
        this.togglePressed(this.plusIcon);
      });
    }
  }

  initElement(element: HTMLElement): void {
    if (this.input.value === this.input.min && element === this.minusIcon) {
      this.disable(element);
    }

    if (this.input.value === this.input.max && element === this.plusIcon) {
      this.disable(element);
    }
  }

  stepUpInputValue(): void {
    if (this.container.classList.contains('-disabled')) {
      return;
    }

    const value = Number.isNaN(this.input.valueAsNumber)
      ? 0
      : this.input.valueAsNumber;

    const step: number = +this.input.step;
    let update = value + step;

    this.enable(this.minusIcon);

    if (update >= +this.input.max) {
      this.disable(this.plusIcon);
      update = +this.input.max;
    }

    this.input.value = update.toString();
  }

  stepDownInputValue(): void {
    if (this.container.classList.contains('-disabled')) {
      return;
    }

    const value = Number.isNaN(this.input.valueAsNumber)
      ? 0
      : this.input.valueAsNumber;

    const step: number = +this.input.step;
    let update = value - step;

    this.enable(this.plusIcon);

    if (update <= +this.input.min) {
      this.disable(this.minusIcon);
      update = +this.input.min;
    }

    this.input.value = update.toString();
  }

  disable(element: HTMLElement): void {
    element.classList.add(DISABLED);
  }

  enable(element: HTMLElement): void {
    element.classList.remove(DISABLED);
  }

  togglePressed(element: HTMLElement): void {
    if (this.container.classList.contains('-disabled')) {
      return;
    }
    if (element.classList.contains(PRESSED)) {
      element.classList.remove(PRESSED);
    } else {
      element.classList.add(PRESSED);
    }
  }
}
