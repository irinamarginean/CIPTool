import WindowWithFrontendKit from '../../WindowWithFrontendKit';

export default (): void => {
  const { boschFrontendKit } = (window as unknown) as WindowWithFrontendKit;

  const examples = document.querySelectorAll(
    '.frontend-kit-example_banner-notification',
  );
  [...examples].forEach(container => {
    const notificationId = container.getAttribute('data-frok-notification');

    const notificationElement = boschFrontendKit.Notification.findNotification(
      notificationId,
    );
    const notification = notificationElement.component;

    const trigger = container.querySelector('[data-frok-action="show"]');

    trigger.addEventListener('click', () =>
      boschFrontendKit.Notification.showNotification(notificationId),
    );
    notification.addEventListener('closeClicked', () => notification.hide());
  });
};
