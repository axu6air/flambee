import React, { Component } from "react";

class CreatePost extends Component {
  state = {};
  componentDidMount() {
    const postImage = { virtualPath: "", Title: "test title" };
    const posts = [
      {
        id: "",
        uploadTime: Date.Now,
        modifiedTime: Date.Now,
        title: "",
        sharedFrom: null,
        postImages: [postImage],
      },
    ];

    return posts;
  }
  render() {
    return <div>HI</div>;
  }
}

export default CreatePost;
