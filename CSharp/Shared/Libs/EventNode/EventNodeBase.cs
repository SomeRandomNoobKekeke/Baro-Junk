using System;
using System.Collections.Generic;
using System.Linq;

namespace BaroJunk
{
  public abstract class EventNodeBase
  {
    public List<ClearableEventBase> Gates { get; } = new();

    public abstract void Map(EventNodeBase next);
    public void Route(EventNodeBase prev) => prev.Map(this);
  }
}