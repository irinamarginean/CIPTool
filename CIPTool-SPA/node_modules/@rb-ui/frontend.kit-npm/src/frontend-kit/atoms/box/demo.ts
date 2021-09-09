import WindowWithFrontendKit from '../../WindowWithFrontendKit';

export default (): void => {
  const { boschFrontendKit } = (window as unknown) as WindowWithFrontendKit;

  // every button with the right class will show the "demo" modal on click
  const modalShowButtons = document.getElementsByClassName(
    'frontend-kit-example_modal-box',
  );
  [...modalShowButtons].forEach(container => {
    const showTrigger = container.querySelector(
      '.a-button[data-frok-action="show"]',
    );

    const modalId = container.getAttribute('data-frok-show-modal');

    const modalElement = boschFrontendKit.Box.findModal(modalId);

    const modal = modalElement.component;

    // close the modal when the background is clicked
    modal.addEventListener('backgroundClicked', () => modal.hide());

    modalElement.addEventListener('click', () =>
      boschFrontendKit.Box.hideModal(modalId),
    );

    if (showTrigger) {
      showTrigger.addEventListener('click', () =>
        boschFrontendKit.Box.showModal(modalId),
      );
    }
  });
};
