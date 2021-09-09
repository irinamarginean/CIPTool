import ElementWithComponent from '../../ElementWithComponent';
import Popover from '.';

export default (): void => {
  // every button with the right class will show the "demo" modal on click
  const examples = document.getElementsByClassName(
    'frontend-kit-example_attached-popover',
  );
  [...examples].forEach(container => {
    const showTrigger = container.querySelector(
      '.a-button[data-frok-action="show"]',
    ) as HTMLElement;

    const popoverElement = container.querySelector(
      '.m-popover',
    ) as ElementWithComponent<Popover>;

    if (!popoverElement) {
      console.warn('Could not find a popover inside the example - skipping.');
      return;
    }

    const popover = popoverElement.component;

    popover.attach(showTrigger);

    showTrigger.addEventListener('click', () => popover.show());

    popover.addEventListener('buttonClicked', () => popover.hide());
    popover.addEventListener('closeButtonClicked', () => popover.hide());
  });

  const galleryExamples = document.querySelectorAll(
    '.frontend-kit-example_attached-popover-gallery',
  );
  [...galleryExamples].forEach(galleryExample => {
    const singleExamples = galleryExample.children;

    [...singleExamples].forEach(example => {
      const trigger = [...example.children].filter(child =>
        child.classList.contains('a-button'),
      )[0] as HTMLElement;

      const popoverElement = [...example.children].filter(child =>
        child.classList.contains('m-popover'),
      )[0] as ElementWithComponent<Popover>;

      if (!popoverElement) {
        console.warn(
          'Could not find a popover inside the gallery example - skipping.',
        );
        return;
      }

      const popover = popoverElement.component;

      popover.attach(trigger);

      trigger.addEventListener('click', () => {
        if (popoverElement.classList.contains('-show')) {
          popover.hide();
        } else {
          popover.show();
        }
      });

      popover.addEventListener('buttonClicked', () => popover.hide());
      popover.addEventListener('closeButtonClicked', () => popover.hide());
    });
  });
};
