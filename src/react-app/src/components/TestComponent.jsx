import React from "react";

class Test extends React.Component {
  state = {
    name: "test",
  };

  static getDerivedStateFromProps(props, state) {
    console.log(props);
    console.log(state);
    if (props.name !== state.name) {
      //Change in props
      return {
        name: props.name,
      };
    }
    return null; // No change to state
  }

  getSnapshotBeforeUpdate(prevProps, prevState) {
    console.log("getSnapshotBeforeUpdate");
    // Are we adding new items to the list?
    // Capture the scroll position so we can adjust scroll later.
    if (prevProps.name !== prevState.name) {
      //const list = this.listRef.current;
      //return list.scrollHeight - list.scrollTop;
      console.log(prevProps);
      console.log(prevState);
    }
    return null;
  }

  componentDidUpdate(prevProps, prevState, snapshot) {
    console.log("componentDidUpdate");
    console.log(prevProps);
    console.log(prevState);
    console.log(snapshot);
    // If we have a snapshot value, we've just added new items.
    // Adjust scroll so these new items don't push the old ones out of view.
    // (snapshot here is the value returned from getSnapshotBeforeUpdate)
    if (snapshot !== null) {
      // const list = this.listRef.current;
      // list.scrollTop = list.scrollHeight - snapshot;
    }
  }

  render() {
    return <div>{this.state.name}</div>;
  }
}

export default Test;
