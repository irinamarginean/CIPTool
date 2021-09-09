import ElementWithComponent from '../../ElementWithComponent';
import FormField from './index';

export default (): void => {
  const exampleContainers = document.querySelectorAll(
    '.frontend-kit-example_form-field-notification',
  );

  [...exampleContainers].forEach(container => {
    const exampleButtons = container.querySelectorAll('.a-button');

    const example = container.querySelector(
      '.m-form-field',
    ) as ElementWithComponent<FormField>;

    const formField = example.component;

    [...exampleButtons].forEach(button => {
      const state = button.getAttribute('data-frok-action');
      if (state !== 'reset') {
        button.addEventListener('click', () => {
          formField.setState(state, state);
        });
      } else {
        button.addEventListener('click', () => {
          formField.setState('neutral');
        });
      }
    });
  });
};
