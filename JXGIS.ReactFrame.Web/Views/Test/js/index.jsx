//ReactDOM.render(
//      <div>
//        <antd.Button type="primary">Primary</antd.Button>
//        <antd.Button>Default</antd.Button>
//        <antd.Button type="ghost">Ghost</antd.Button>
//        <antd.Button type="dashed">Dashed</antd.Button>
//      </div>,
//    document.getElementById('app')
//);

const App = React.createClass({
    getInitialState() {
        return {
            loading: false,
            iconLoading: false,
        };
    },
    enterLoading() {
        this.setState({ loading: true });
    },
    enterIconLoading() {
        this.setState({ iconLoading: true });
    },
    render() {
        return (
          <div>
            <antd.Button type="primary" loading>
              Loading
            </antd.Button>
            <antd.Button type="primary" size="small" loading>
              Loading
            </antd.Button>
            <br />
            <antd.Button type="primary" loading={this.state.loading} onClick={this.enterLoading}>
              Click me!
            </antd.Button>
            <antd.Button type="primary" icon="poweroff" loading={this.state.iconLoading} onClick={this.enterIconLoading}>
              Click me!
            </antd.Button>
          </div>
      );
    },
});

ReactDOM.render(<App />, document.getElementById('app'));