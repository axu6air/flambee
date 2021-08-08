const EventBus = {
  on(event, callback) {
    console.log("EventBus on");
    document.addEventListener(event, (e) => callback(e.detail));
  },
  dispatch(event, data) {
    console.log("EventBus dispatch");
    document.dispatchEvent(new CustomEvent(event, { detail: data }));
  },
  remove(event, callback) {
    console.log("EventBus remove");
    document.removeEventListener(event, callback);
  },
};

export default EventBus;
