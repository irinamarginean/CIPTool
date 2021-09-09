import BaseComponent from '../../baseComponent';

const TEXTAREA = 'textarea';

export default class TextArea extends BaseComponent {
  protected static events = ['onInput'];

  private textArea: HTMLTextAreaElement;

  private shadow: HTMLDivElement;

  constructor(container: HTMLElement) {
    super(container);

    /**
     * Define DOM Elements and Variables
     */
    this.textArea = container.querySelector(TEXTAREA);

    this.shadow = container.querySelector('.a-text-area__shadow');

    if (this.shadow && this.textArea instanceof HTMLTextAreaElement) {
      // register change event for the textarea
      this.textArea.addEventListener('input', () => {
        this.shadow.textContent = `${this.textArea.value}\r\n`;
        this.textArea.style.height = `${this.shadow.offsetHeight}px`;
      });
    }
  }
}
