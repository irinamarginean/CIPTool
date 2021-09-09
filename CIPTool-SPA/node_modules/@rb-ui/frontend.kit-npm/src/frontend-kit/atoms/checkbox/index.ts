import BaseComponent from '../../baseComponent';

const INDETERMINATE_CLASS = 'a-checkbox--indeterminate';

export default class Checkbox extends BaseComponent {
  private input: HTMLInputElement;

  constructor(container: HTMLElement) {
    super(container);

    this.input = container.querySelector('input');

    if (this.input) {
      this.input.addEventListener('change', () => {
        container.classList.remove(INDETERMINATE_CLASS);
      });
    }
  }
}
